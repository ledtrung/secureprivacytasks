import { Routes } from '@angular/router';
import { UserListComponent } from './users/list/list.component';
import { UserAddComponent } from './users/add/add.component';

export const routes: Routes = [
    { path: '', component: UserListComponent },
    { path: 'users', component: UserListComponent },
    { path: 'users/add', component: UserAddComponent }
];
