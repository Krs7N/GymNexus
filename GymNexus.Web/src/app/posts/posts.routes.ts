import { Route } from "@angular/router";
import { PostsComponent } from "./posts/posts.component";

export const postsRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        component: PostsComponent
    }
]