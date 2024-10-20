import { Component, OnInit } from '@angular/core';
import { User } from '../user'
import { UserService } from '../user.service'
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'user-list',
  standalone: true,
  templateUrl: './list.component.html',
  imports: [ CommonModule, FormsModule, RouterOutlet ]
})
export class UserListComponent implements OnInit {
    users: User[] = [];

    ngOnInit(): void {
        this.refreshUsers();
    }

    constructor(private userService: UserService) {}

    refreshUsers() { 
        this.userService.getUsers().subscribe((users: User[]) => { 
            this.users = users;
        });
    }
}
