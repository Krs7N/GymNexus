import { Route } from "@angular/router";
import { ProductsComponent } from "./products/products.component";
import { inject } from "@angular/core";
import { ProfileService } from "../auth/services/profile.service";

export const productsRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        component: ProductsComponent,
        resolve: {
            userStores: () => inject(ProfileService).getUserStores()
        }
    }
];