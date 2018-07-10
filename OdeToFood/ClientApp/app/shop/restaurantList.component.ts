import { Component, OnInit } from "@angular/core";
import { DataService } from "../shared/dataService";
import { Restaurant } from "../shared/restaurant";

@Component({
    selector: "restaurant-list",
    templateUrl: "restaurantList.component.html",
    styleUrls: []
})
export class RestaurantList implements OnInit {
    public restaurants: Restaurant[] = [];

    constructor(private data: DataService) {
        this.restaurants = data.restaurants;
    }

    ngOnInit(): void {
        this.data.loadRestaurants()
            .subscribe(success => {
                if (success) {
                    this.restaurants = this.data.restaurants;
                }
            })
    }
}