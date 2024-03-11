import { CommentViewModel } from "./comment-view-model";

export class PostViewModel {
    id: number;
    title: string;
    content: string;
    imageUrl?: string;
    createdOn: string;
    createdBy: string;
    likes: number = 0;
    isLikedByCurrentUser: boolean;
    comments: CommentViewModel[] = [];

    constructor(
        id: number,
        title: string,
        content: string,
        createdOn: string,
        createdBy: string,
        likes: number,
        comments: CommentViewModel[],
        isLikedByCurrentUser: boolean,
        imageUrl?: string) {
        this.id = id;
        this.title = title;
        this.content = content;
        this.imageUrl = imageUrl;
        this.createdOn = createdOn;
        this.createdBy = createdBy;
        this.likes = likes;
        this.isLikedByCurrentUser = isLikedByCurrentUser;
        this.comments = comments;
    }
}
