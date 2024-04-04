import { Route } from "@angular/router";
import { ProductsComponent } from "./products/products.component";
import { inject } from "@angular/core";
import { ProfileService } from "../auth/services/profile.service";
import { ProductDetailsComponent } from "./product-details/product-details.component";
import { NomenclatureService } from "../shared/services/nomenclature.service";
import { MarketplaceService } from "../map/marketplace-map/marketplace.service";
import { Actions } from "../enums/actions";

export const productsRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        component: ProductsComponent,
        resolve: {
            userStores: () => inject(ProfileService).getUserStores(),
            categories: () => inject(NomenclatureService).getCategories()
        }
    },
    {
        path: 'add',
        component: ProductDetailsComponent,
        data: {
            action: Actions.CREATE
        },
        resolve: {
            categories: () => inject(NomenclatureService).getCategories(),
            marketplaces: () => inject(MarketplaceService).getAllMarketplaces()
        }
    },
    {
        path: ':id',
        component: ProductDetailsComponent,
        data: {
            action: Actions.EDIT
        },
        resolve: {
            categories: () => inject(NomenclatureService).getCategories(),
            marketplaces: () => inject(MarketplaceService).getAllMarketplacesWithStores()
        }
    }
];