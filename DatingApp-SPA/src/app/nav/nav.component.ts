import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};

  constructor(
    public authService: AuthService,
    private alertifyService: AlertifyService,
    private router: Router
  ) { }

  ngOnInit() {
  }

  login() {
    this.authService.login(this.model)
    .subscribe(next => {
      this.alertifyService.success('Logged In!');
    }, error => {
      this.alertifyService.error(error);
    }, () => {
      this.router.navigate(['/members']);
    });
  }

  loggedIn() {
    return this.authService.loggedIn(); // check token for expiry date.
  }

  logout() {
    localStorage.removeItem('Token');
    this.alertifyService.message('Logged Out.');
    this.router.navigate(['/home']);
  }

}
