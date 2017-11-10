import {Component, OnInit, ViewEncapsulation} from '@angular/core';
import {AuthService} from '../../../service/auth.service';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {Router} from '@angular/router';

@Component({
    selector: 'zli-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss'],
    encapsulation: ViewEncapsulation.Emulated
})
export class LoginComponent implements OnInit {
    public loginForm: FormGroup;
    public errorMessage: string;

    constructor(private service: AuthService,
                private formBuilder: FormBuilder,
                private router: Router) {
        this.loginForm = this.formBuilder.group({
            username: ['', Validators.required],
            password: ['', Validators.required]
        });
    }

    public ngOnInit(): void {
    }

    public onSubmit(): void {
        const formModel = this.loginForm.value;
        this.errorMessage = null;

        this.service.login(formModel.username, formModel.password)
            .subscribe(
                (user) => {
                    if (!!user) {
                        this.router.navigate(['/books']);
                    } else {
                        this.errorMessage = 'Invalid username or password';
                    }
                }, error => {
                    this.errorMessage = error;
                }
            );
    }
}
