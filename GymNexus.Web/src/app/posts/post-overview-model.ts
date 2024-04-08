export class PostOverviewModel {
    id: number;
    title: string;
    content: string;
    likes?: number;
    comments?: number;

    constructor(id: number, title: string, content: string) {
        this.id = id;
        this.title = title;
        this.content = content;
    }
}