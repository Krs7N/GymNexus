import { Route } from "@angular/router";
import { ProductsComponent } from "./products/products.component";
import { inject } from "@angular/core";
import { ProfileService } from "../auth/services/profile.service";
import { ProductDetailsComponent } from "./product-details/product-details.component";
import { NomenclatureService } from "../shared/services/nomenclature.service";
import { MarketplaceService } from "../map/marketplace-map/marketplace.service";

export const productsRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        component: ProductsComponent,
        resolve: {
            userStores: () => inject(ProfileService).getUserStores()
        }
    },
    {
        path: ':id',
        component: ProductDetailsComponent,
        resolve: {
            categories: () => inject(NomenclatureService).getCategories(),
            marketplaces: () => inject(MarketplaceService).getAllMarketplacesWithStores()
        }
    }
];