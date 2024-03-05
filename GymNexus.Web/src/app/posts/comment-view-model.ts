export class CommentViewModel {
    content: string;
    createdOn: string;
    createdBy: string;
    
    constructor(content: string, createdOn: string, createdBy: string) {
        this.content = content;
        this.createdOn = createdOn;
        this.createdBy = createdBy;
    }
}
