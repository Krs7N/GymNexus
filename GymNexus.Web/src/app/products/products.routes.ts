import { Route } from "@angular/router";
import { ProductsComponent } from "./products/products.component";

export const productsRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        component: ProductsComponent
    }
];