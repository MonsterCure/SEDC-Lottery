import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IUserCodeAward } from './winners-list.model';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class WinnersListService {
    winnersUrl: string = 'http://localhost:59060/api/lottery/';

    constructor(private httpClient: HttpClient) {


    }

    getAllWinners(): Observable<Array<IUserCodeAward>> {
        return this.httpClient.get<Array<IUserCodeAward>>(this.winnersUrl + "getAllWinners");
    }
}