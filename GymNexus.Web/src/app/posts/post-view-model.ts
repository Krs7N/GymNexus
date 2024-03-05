import { CommentViewModel } from "./comment-view-model";

export class PostViewModel {
    title: string;
    content: string;
    imageUrl?: string;
    createdOn: string;
    createdBy: string;
    likes: number = 0;
    comments: CommentViewModel[] = [];

    constructor(title: string,
        content: string,
        createdOn: string,
        createdBy: string,
        likes: number,
        comments: CommentViewModel[],
        imageUrl?: string) {
        this.title = title;
        this.content = content;
        this.imageUrl = imageUrl;
        this.createdOn = createdOn;
        this.createdBy = createdBy;
        this.likes = likes;
        this.comments = comments;
    }
}
