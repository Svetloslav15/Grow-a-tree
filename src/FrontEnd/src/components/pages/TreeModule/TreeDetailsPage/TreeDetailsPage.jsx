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

const ReportButton = require('../../../../assets/report-button.svg');

const TreeDetailsPage = ({history, match}) => {
    const [tree, setTree] = useState([]);
    const [posts, setPosts] = useState([]);
    const [currPostPage, setCurrPostPage] = useState(1);
    const [postsLimitPerPage, setPostsLimitPerPage] = useState(1000);
    const [editorKey, setEditorKey] = useState(4);
    const [treeLocation, setLocation] = useState('');
    const [currPost, setCurrPost] = useState({id: ''});

    const currUser = useSelector(state => state.auth);

    useEffect(() => {
        fetchTreeInfo();
        fetchTreePosts();
    }, []);

    const fetchTreeInfo =() => {
        TreeService.getAuthorizedTreeById(match.params.id, currUser.accessToken)
            .then((data) => {
                const treeInfo = data.data.data;
                setTree(treeInfo);
                setCurrPost({treeId: treeInfo.id, ...currPost});
                GeoCodingService.getCityByCoords(treeInfo.latitude, treeInfo.longitude)
                    .then((res) => {
                        const {municipality, suburb, village} = res.data.address;
                        const result = [municipality, suburb, village]
                            .filter(x => x !== undefined)
                            .map(x => `${x} `);
                        setLocation(result);
                    });
            });
    }

    const fetchTreePosts = () => {
        TreeService.getAuthorizedTreePosts(`?page=${currPostPage}&perPage=${postsLimitPerPage}`, currUser.accessToken)
            .then((response) => {
                if (response.data.succeeded) {
                    fetchTreePostReactions(response.data.data)
                        .then((postsWithReactions) => {
                            setPosts([...postsWithReactions]);
                        });
                }
            });
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
        setCurrPost({ ...currPost, content: data});
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
                {posts.length}
                {
                    posts && posts.map((data, index) => <TreePost key={index}
                                                                 data={data}
                                                                 fetchTreePosts={fetchTreePosts}/>)
                }
            </div>
        </Layout>
    )
};
export default TreeDetailsPage;