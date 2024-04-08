import { Route } from "@angular/router";
import { AdminDashboardComponent } from "./admin-dashboard/admin-dashboard.component";
import { AdminManageOrdersComponent } from "./admin-manage-orders/admin-manage-orders.component";
import { inject } from "@angular/core";
import { AdminService } from "./admin.service";
import { AdminManagePostsComponent } from "./admin-manage-posts/admin-manage-posts.component";

export const adminRoutes: Route[] = [
    {
        path: 'dashboard',
        component: AdminDashboardComponent,
    },
    {
        path: 'manage/orders',
        component: AdminManageOrdersComponent,
        resolve: {
            orders: () => inject(AdminService).getAllOrders()
        }
    },
    {
        path: 'manage/posts',
        component: AdminManagePostsComponent,
        resolve: {
            mostLikedPost: () => inject(AdminService).getMostLikedPost(),
            mostCommentedPost: () => inject(AdminService).getMostCommentedPost()
        }
    }
]