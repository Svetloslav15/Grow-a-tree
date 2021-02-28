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
    postAuthorizedUpsertTreeReplyReact: '/treePostReplyReactions/upsert',
    getAuthorizedTreePostReplyReactions: '/treePostReplyReactions/list',
    getNearestTrees: '/trees/closest-trees-short-info',
    getAuthorizedActiveReportTypes: '/treeReports/active-reports-types',
    getAuthorizedArchivedReportTypes: '/treeReports/archived-reports-types',
    getAuthorizedActiveReports: '/treeReports/active-reports',
    getAuthorizedArchivedReports: '/treeReports/archived-reports',
    getAuthorizedTreesByUser: '/trees/user',
    postAuthorizedArchiveReports: '/treeReports/archive-report',
    postAuthorizedMakeSpamReport: '/treeReports/mark-as-spam',
    getRecentTrees: '/trees/recent-trees',
    postAuthorizedDeletePost: '/treePosts/delete',
    postFormPredictTreeLeaf: '/ml/predict-leaf'
};

export default new Proxy({}, {
    get(target, propName) {
        if (propName.startsWith('postAuthorized')) {
            return async (data, token, contentType) => await baseService.postAuthorized(ROUTES[propName], data, token, contentType);
        }
        if (propName.startsWith('postForm')) {
            return async (data, contentType) => await baseService.post(ROUTES[propName], data, contentType);
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