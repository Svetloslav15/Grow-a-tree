import React, {useEffect, useState} from 'react';
import {useSelector} from 'react-redux';
import {Editor} from '@tinymce/tinymce-react';

import './TreeDetailsPage.scss';

import Layout from '../../../common/Layout/Layout';
import Button from '../../../common/Button/Button';
import Carousel from './Carousel/Carousel';
import TreeService from '../../../../services/treeService';
import AlertService from '../../../../services/alertService';
import SuccessMessages from '../../../../static/successMessages';
import Map from '../../../common/Map/Map';
import Timer from './Timer/Timer';
import TreePost from './TreePost/TreePost';

import GeoCodingService from '../../../../services/geocodingService';
import ReactionButton from '../../../common/ReactionButton/ReactionButton';
import ListModal from "./ListModal/ListModal";

const ReportButton = require('../../../../assets/report-button.svg');
const HeartIcon = require('../../../../assets/reaction-heart.png');
const DropIcon = require('../../../../assets/drop.png');

const TreeDetailsPage = ({history, match}) => {
    const [tree, setTree] = useState([]);
    const [posts, setPosts] = useState([]);
    const [currPostPage, setCurrPostPage] = useState(1);
    const [postsLimitPerPage, setPostsLimitPerPage] = useState(1000);
    const [editorKey, setEditorKey] = useState(4);
    const [treeLocation, setLocation] = useState('');
    const [currPost, setCurrPost] = useState({id: ''});
    const [isWateringModalOpen, toggleIsWateringModalOpen] = useState(false);

    const currUser = useSelector(state => state.auth);

    useEffect(() => {
        loadData();
    }, []);

    const loadData = async () => {
        await fetchTreeInfo();
        await fetchTreePosts();
    }

    const fetchTreeInfo = async () => {
        const data = await TreeService.getAuthorizedTreeById(match.params.id, currUser.accessToken);
        const treeInfo = data.data.data;
        setTree(treeInfo);
        document.title = `Grow A Tree - ${treeInfo.nickname}`
        setCurrPost({treeId: treeInfo.id, ...currPost});
        const res = await GeoCodingService.getCityByCoords(treeInfo.latitude, treeInfo.longitude);
        const {municipality, suburb, village} = res.data.address;
        const result = [municipality, suburb, village]
            .filter(x => x !== undefined)
            .map(x => `${x} `);
        setLocation(result);
    }

    const fetchTreePosts = async () => {
        const response = await TreeService.getAuthorizedTreePosts(`?page=${currPostPage}&perPage=${postsLimitPerPage}`, currUser.accessToken);
        if (response.data.succeeded) {
            const postsWithReactions = await fetchTreePostReactions(response.data.data);
            setPosts(postsWithReactions);
        }
    }

    const fetchTreeWaterings = async () => {
        const waterings = await TreeService.getAuthorizedTreeWaterings(`?treeId=${tree.id}&perPage=20&page=1`, currUser.accessToken)
    }

    const fetchTreePostReactions = async (data) => {
        for (const post of data) {
            const response = await TreeService.getAuthorizedTreePostReactions(`?postId=${post.id}`, currUser.accessToken);
            post.reactions = response.data.data;
        }
        return data;
    }

    const addPost = () => {
        TreeService.postAuthorizedUpsertTreePost(currPost, currUser.accessToken)
            .then((response) => {
                if (response.succeeded) {
                    AlertService.success(SuccessMessages.successAddedTreePost);
                    setCurrPost({id: ''});
                    fetchTreePosts();
                    //Clear editor content
                    const newKey = editorKey * 43;
                    setEditorKey(newKey);
                } else {
                    AlertService.error(response.errors[0]);
                }
            });
    }

    const handleEditorChange = async (data) => {
        setCurrPost({...currPost, content: data});
    }

    const waterTree = async () => {
        const waterTreeData = {
            treeId: tree.id,
            watererId: currUser.id
        };

        const response = await TreeService.postAuthorizedWaterTree(waterTreeData, currUser.accessToken);
        if (response.succeeded) {
            await AlertService.success(SuccessMessages.successWaterTree);
        }
        else {
            await AlertService.error(response.errors[0]);
        }
    }

    return (
        <Layout>
            <section className='info-section'>
                {tree.images && <Carousel images={tree.images}/>}
                <section className='info-section__wrapper'>
                    <h1 className='info-section__wrapper__title'># {tree.nickname}</h1>
                    <p className='info-section__wrapper__status'>Статус: здраво</p>
                    <p className='info-section__wrapper__status'>Вид: {tree.type}</p>
                    <p className='info-section__wrapper__owner'>Засадено от: {tree.owner && tree.owner.userName}</p>
                    <div className='row'>
                        <div className='action-section mr-4'>
                            <div className='action-section__item'>
                                <span className='action-section__item__counter'>89</span>
                                <img  className='action-section__item__image' src={HeartIcon} alt=""/>
                            </div>
                            <Button type='DarkOutline'>Реагирай</Button>
                        </div>
                        <div className='action-section ml-4'>
                            <div className='action-section__item' onClick={() => toggleIsWateringModalOpen(!isWateringModalOpen)}>
                                <span className='action-section__item__counter'>89</span>
                                <img  className='action-section__item__image' src={DropIcon} alt=""/>
                            </div>
                            <Button type='DarkOutline' onClick={waterTree}>Полей</Button>
                            {
                                isWateringModalOpen && (<ListModal closeModal={() => toggleIsWateringModalOpen(false)}/>)
                            }
                        </div>
                    </div>
                </section>
                <img className='info-section__report-button'
                     src={ReportButton}
                     alt="Report Problem Button"/>
            </section>
            <section className='info-section__map'>
                <Timer/>
                <p className='info-section__map__location-text'>{treeLocation}</p>
                <div className='info-section__map-wrapper'>
                    <Map coordinates={{latitude: tree.latitude, longitude: tree.longitude}}
                         isStatic={true}
                         className='map-container-section'/>
                </div>
            </section>
            <div className='info-section__posts'>
                <Editor
                    key={editorKey}
                    apiKey={process.env.REACT_APP_TINYMCE_KEY}
                    init={{
                        height: 200,
                        menubar: false,
                        plugins: [
                            'advlist autolink lists link image charmap print preview anchor',
                            'searchreplace visualblocks code fullscreen',
                            'insertdatetime media table paste code help wordcount imagetools'
                        ],
                        toolbar:
                            'formatselect  | image link | bold italic backcolor | \
                            alignleft aligncenter alignright alignjustify',
                        file_picker_types: 'file image',
                    }}
                    onEditorChange={handleEditorChange}
                />
                <Button type='DarkOutline' onClick={addPost}>Добави</Button>
                {
                    posts.map((data, index) => <TreePost key={index}
                                                         data={data}
                                                         fetchTreePosts={fetchTreePosts}/>)
                }
            </div>
        </Layout>
    )
};
export default TreeDetailsPage;