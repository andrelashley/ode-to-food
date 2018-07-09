import { Component } from "@angular/core";

@Component({
    selector: "restaurant-list",
    templateUrl: "restaurantList.component.html",
    styleUrls: []
})
export class RestaurantList {
    public restaurants = [{
        name: "Kukoo's",
        rating: 5.5
    },
    {
        name: "Shawarma Palace",
        rating: 7
    },
    {
        name: "5th Street Bar and Grill",
        rating: 10
    }];
}