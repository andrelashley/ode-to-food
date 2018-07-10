import { HttpClient } from "@angular/common/http"
import { Injectable } from "@angular/core";
import 'rxjs/add/operator/map';
import { Observable } from "rxjs";
import { Restaurant } from "../shared/restaurant";

@Injectable()
export class DataService {

    public restaurants: Restaurant[] = [];

    constructor(private http: HttpClient) { }

    loadRestaurants(): Observable<boolean> {
        return this.http.get("/api/restaurants")
            .map((data: any[]) => {
                this.restaurants = data;
                return true;
            });
    }

}