import React, {useEffect, useState} from 'react';
import {useSelector} from 'react-redux';
import {Editor} from '@tinymce/tinymce-react';

import styles from './TreeDetailsPage.module.scss';

import Layout from '../../../common/Layout/Layout';
import Button from '../../../common/Button/Button';
import Carousel from './Carousel/Carousel';
import TreeService from '../../../../services/treeService';
import AlertService from '../../../../services/alertService';
import SuccessMessages from '../../../../static/successMessages';
import Map from '../../../common/Map/Map';
import Timer from './Timer/Timer';
import TreePost from './TreePost/TreePost';
import ReportModal from './ReportModal/ReportModal';
import ListModal from './ListModal/ListModal';
import WateringModal from './WateringModal/WateringModal';

import GeoCodingService from '../../../../services/geocodingService';
import ReactionButton from '../../../common/ReactionButton/ReactionButton';

const ReportButton = require('../../../../assets/report-button.svg');
const HeartIcon = require('../../../../assets/reaction-heart.png');
const SadIcon = require('../../../../assets/reaction-sad.png');
const LikeIcon = require('../../../../assets/reaction-like.png');
const LaughIcon = require('../../../../assets/reaction-laugh.png');
const DropIcon = require('../../../../assets/drop.png');

const WATERINGS_PER_PAGE = 20;
let openUndoIsOpen = false;

const TreeDetailsPage = ({history, match}) => {
    const [tree, setTree] = useState([]);
    const [posts, setPosts] = useState([]);
    const [currPostPage, setCurrPostPage] = useState(1);
    const [postsLimitPerPage, setPostsLimitPerPage] = useState(1000);
    const [editorKey, setEditorKey] = useState(4);
    const [treeLocation, setLocation] = useState('');
    const [currPost, setCurrPost] = useState({id: ''});
    const [isWateringModalOpen, toggleIsWateringModalOpen] = useState(false);
    const [isReportModalOpen, toggleIsReportModalOpen] = useState(false);
    const [isReactionsModalOpen, toggleIsReactionsModalOpen] = useState(false);
    const [isSuccessWateringModalOpen, toggleIsSuccessWateringModalOpen] = useState(false);
    const [isUndoOpen, setUndoIsOpen] = useState(false);
    const [isPageLoaded, setPageLoaded] = useState(false);
    const currUser = useSelector(state => state.auth);
    const [currWateringPage, setCurrWateringPage] = useState(1);
    const [treeWaterings, setTreeWaterings] = useState([]);
    const [treeReactions, setTreeReactions] = useState([]);
    const [reactionTypes, setCurrReactionTypes] = useState([]);

    useEffect(() => {
        if (!isPageLoaded) {
            loadData();
            setPageLoaded(true);
        }
        checkReactionTypes();
    }, [treeReactions]);

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

        await fetchTreeWaterings(treeInfo.id);
        await fetchTreeReactions(treeInfo.id);
    }

    const fetchTreePosts = async () => {
        const response = await TreeService.getAuthorizedTreePosts(`?page=${currPostPage}&perPage=${postsLimitPerPage}`, currUser.accessToken);
        if (response.data.succeeded) {
            const postsWithReactions = await fetchTreePostReactions(response.data.data);
            setPosts(postsWithReactions);
        }
    }

    const fetchTreeReactions = async (treeId) => {
        const response = await TreeService.getAuthorizedTreeReactons(`?treeId=${treeId}&perPage=2000`, currUser.accessToken);
        if (response.data.succeeded) {
            setTreeReactions(response.data.data);
            checkReactionTypes();
        }
    }

    const fetchTreeWaterings = async (treeId) => {
        const response = await TreeService.getAuthorizedTreeWaterings(`?treeId=${treeId}&perPage=${WATERINGS_PER_PAGE}&page=${currWateringPage}`, currUser.accessToken);

        if (response.data.succeeded) {
            setTreeWaterings(response.data.data);
        } else {
            await AlertService.error(response.errors[0]);
        }
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

    const undoWateringATree = async () => {
        setUndoIsOpen(false);
        openUndoIsOpen = false;
    }

    const waterTree = async () => {
        setUndoIsOpen(true);
        openUndoIsOpen = true;

        setTimeout(async () => {
            const waterTreeData = {
                treeId: tree.id,
                watererId: currUser.id
            };

            if (openUndoIsOpen) {
                const response = await TreeService.postAuthorizedWaterTree(waterTreeData, currUser.accessToken);
                if (response.succeeded) {
                    await AlertService.success(SuccessMessages.successWaterTree);
                    await fetchTreeWaterings();
                    toggleIsSuccessWateringModalOpen(true);
                } else {
                    await AlertService.error(response.errors[0]);
                }
                setUndoIsOpen(false);
            }

            openUndoIsOpen = false;
        }, 5000)
    }

    const reactToTree = async (reactionType) => {
        const data = {
            type: reactionType,
            userId: currUser.id,
            treeId: tree.id
        };

        const response = await TreeService.postAuthorizedReactToTree(data, currUser.accessToken);
        if (response.succeeded) {
            AlertService.success(SuccessMessages.successReactedToTree);
            await fetchTreeReactions(tree.id);
        } else {
            AlertService.error(response.errors[0]);
        }
    }

    const checkReactionTypes = () => {
        const currImages = [];
        if (treeReactions) {
            if (treeReactions.filter(x => x.type === 1).length > 0) {
                currImages.push(HeartIcon);
            }
            if (treeReactions.filter(x => x.type === 2).length > 0) {
                currImages.push(LaughIcon);
            }
            if (treeReactions.filter(x => x.type === 3).length > 0) {
                currImages.push(LikeIcon);
            }
            if (treeReactions.filter(x => x.type === 4).length > 0) {
                currImages.push(SadIcon);
            }
        }
        setCurrReactionTypes(currImages);
    }

    return (
        <Layout>
            <section className={styles.infoSection}>
                {tree.images && <Carousel images={tree.images}/>}
                <section className={styles.infoSection__wrapper}>
                    <h1 className={styles.infoSection__wrapper__title}># {tree.nickname}</h1>
                    <p className={styles.infoSection__wrapper__status}>Статус: здраво</p>
                    <p className={styles.infoSection__wrapper__status}>Вид: {tree.type}</p>
                    <p className={styles.infoSection__wrapper__owner}>Засадено
                        от: {tree.owner && tree.owner.userName}</p>
                    <div className='row'>
                        <div className={`${styles.actionSection} mr-4`}>
                            <div className={styles.actionSection__item}
                                 onClick={() => toggleIsReactionsModalOpen(!isWateringModalOpen)}>
                                <span className={styles.actionSection__item__counter}>{treeReactions.length}</span>
                                <div className={styles.actionSection__item__imagesSection}>
                                    {
                                        reactionTypes && reactionTypes.map(x => <img
                                            className={styles.actionSection__item__image__reaction} src={x}
                                            alt="Reaction Icon"/>)
                                    }
                                </div>
                            </div>
                            <ReactionButton reactTo={reactToTree} item={{reactions: treeReactions}}
                                            reactionsVisible={false} hasBorder={false}>Реагирай</ReactionButton>
                            {
                                isReactionsModalOpen && (<ListModal data={treeReactions}
                                                                    closeModal={() => toggleIsReactionsModalOpen(false)}
                                                                    hasReaction={true}
                                                                    title={`Реакции за ${tree.nickname}`}
                                />)
                            }
                        </div>
                        <div className={`${styles.actionSection} ml-4`}>
                            <div className={styles.actionSection__item}
                                 onClick={() => toggleIsWateringModalOpen(!isWateringModalOpen)}>
                                <span className={styles.actionSection__item__counter}>{treeWaterings.length}</span>
                                <img className={styles.actionSection__item__image} src={DropIcon} alt=""/>
                            </div>
                            {
                                isUndoOpen ?
                                    <Button type='DarkOutline' onClick={undoWateringATree}>Спри поливането</Button>
                                    :
                                    <Button type='DarkOutline' onClick={waterTree}>Полей</Button>
                            }
                            {
                                isWateringModalOpen && (<ListModal data={treeWaterings}
                                                                   closeModal={() => toggleIsWateringModalOpen(false)}
                                                                   hasReaction={false}
                                                                   title='Последни поливания'
                                />)
                            }
                            {
                                isSuccessWateringModalOpen && (
                                    <WateringModal xp={45}
                                                   closeModal={() => toggleIsSuccessWateringModalOpen(false)}
                                    />)
                            }
                        </div>
                    </div>
                </section>
                <img className={styles.infoSection__reportButton}
                     src={ReportButton}
                     alt="Report Problem Button"
                     onClick={() => toggleIsReportModalOpen(true)}/>
                {isReportModalOpen ? <ReportModal closeModal={() => toggleIsReportModalOpen(false)}/> : ''}
            </section>
            <section className={styles.infoSection__map}>
                <Timer data={treeWaterings[0]}/>
                <p className={styles.infoSection__map__locationText}>{treeLocation}</p>
                <div className={styles.infoSection__mapWrapper}>
                    <Map coordinates={{latitude: tree.latitude, longitude: tree.longitude}}
                         isStatic={true}
                         className={styles.mapContainerSection}/>
                </div>
            </section>
            <div className={styles.infoSection__posts}>
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
                <Button type='DarkOutline'
                        onClick={addPost}>Добави</Button>
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