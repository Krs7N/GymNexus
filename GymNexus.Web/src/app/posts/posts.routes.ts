import { Route } from "@angular/router";
import { PostsComponent } from "./posts/posts.component";
import { PostDetailsComponent } from "./post-details/post-details.component";

export const postsRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        component: PostsComponent
    },
    {
        path: ':id',
        component: PostDetailsComponent
    }
]