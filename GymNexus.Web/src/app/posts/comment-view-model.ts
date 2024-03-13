export class CommentViewModel {
    id?: number;
    content: string;
    createdOn: string;
    createdBy: string;
    isEdited: boolean = false;
    
    constructor(content: string, createdOn: string, createdBy: string, id?: number) {
        this.id = id;
        this.content = content;
        this.createdOn = createdOn;
        this.createdBy = createdBy;
    }
}
