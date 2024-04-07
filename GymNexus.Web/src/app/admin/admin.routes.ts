import { Route } from "@angular/router";
import { AdminDashboardComponent } from "./admin-dashboard/admin-dashboard.component";
import { AdminManageOrdersComponent } from "./admin-manage-orders/admin-manage-orders.component";
import { inject } from "@angular/core";
import { AdminService } from "./admin.service";

export const adminRoutes: Route[] = [
    {
        path: 'dashboard',
        component: AdminDashboardComponent,
    },
    {
        path: 'orders',
        component: AdminManageOrdersComponent,
        resolve: {
            orders: () => inject(AdminService).getAllOrders()
        }
    }
]