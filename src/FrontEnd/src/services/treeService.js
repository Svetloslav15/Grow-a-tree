import baseService from './baseService';

const ROUTES = {
    postAuthorizedAddTree: '/trees/upsert',
    getAuthorizedTreeById: '/trees/',
    getTreesForCarousel: '/trees/random-images',
    postAuthorizedUpsertTreePost: '/treePosts/upsert',
    getAuthorizedTreePosts: '/treePosts/list',
    getAuthorizedTreePostReactions: '/treePostReactions/list',
    postAuthorizedUpsertTreeReaction: '/treePostReactions/upsert',
    postAuthorizedWaterTree: '/waterings/water-tree',
    getAuthorizedTreeWaterings: '/waterings/tree-waterings',
    getAuthorizedTreeReactons: '/treeReactions/tree-reactions',
    postAuthorizedReactToTree: '/treeReactions/upsert',
    postAuthorizedReportTree: '/treeReports/report-tree',
    getAuthorizedTreePostReplies: '/treePostReplies/list',
    postAuthorizedUpsertTreeReply: '/treePostReplies/upsert',
    postAuthorizedUpsertTreeReplyReact: '/treePostReplyReactions/upsert'
};

export default new Proxy({}, {
    get(target, propName) {
        if (propName.startsWith('postAuthorized')) {
            return async (data, token, contentType) => await baseService.postAuthorized(ROUTES[propName], data, token, contentType);
        }
        else if (propName.startsWith('post')) {
            return async (data) => await baseService.post(ROUTES[propName], data);
        }
        else if (propName.startsWith('getAuthorized')) {
            return async (urlParams, token) => await baseService.getAuthorized(`${ROUTES[propName]}${urlParams}`, token);
        }
        return async (urlParams) => await baseService.get(`${ROUTES[propName]}${urlParams}`);
    }
});