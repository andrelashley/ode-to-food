import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from "@angular/common/http";

import { AppComponent } from './app.component';
import { RestaurantList } from './shop/restaurantList.component';
import { DataService } from './shared/dataService';

import { RouterModule } from "@angular/router";
import { Shop } from './shop/shop.component';

let routes = [
    { path: "", component: Shop }
];

@NgModule({
    declarations: [
        AppComponent,
        RestaurantList,
        Shop
    ],
    imports: [
        BrowserModule,
        HttpClientModule,
        RouterModule.forRoot(routes, {
            useHash: true,
            enableTracing: false // for debugging routes
        })
    ],
    providers: [
        DataService
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
