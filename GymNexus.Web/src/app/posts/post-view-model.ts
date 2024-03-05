export class PostViewModel {
    title: string;
    content: string;
    imageUrl?: string;
    createdOn: string;
    createdBy: string;

    constructor(title: string, content: string, createdOn: string, createdBy: string, imageUrl?: string) {
        this.title = title;
        this.content = content;
        this.imageUrl = imageUrl;
        this.createdOn = createdOn;
        this.createdBy = createdBy;
    }
}
