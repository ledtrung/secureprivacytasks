import { Injectable } from '@angular/core';
import {User} from './user'
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class UserService {
    baseUrl: string = "http://localhost:5048/";

    constructor(private http: HttpClient) {
        
    }

    public getUsers() {
        return this.http.get<User[]>(this.baseUrl + 'users');
    }

    public addUser(user: User): Observable<any> { 
        const headers = { 'content-type': 'application/json' }  
        const body = JSON.stringify(user);
        return this.http.post(this.baseUrl + 'users', body, { 'headers': headers });
    }
}