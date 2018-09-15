import { Component, OnInit } from '@angular/core';
import { IUserCode, ICode } from '../winners-list/winners-list.model';
import { SubmitCodeService } from './submit-code.service';
import { ToastrService } from 'ngx-toastr';
import { WinnersListService } from '../winners-list/winners-list.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-submit-code',
  templateUrl: './submit-code.component.html',
  styleUrls: ['./submit-code.component.css']
})
export class SubmitCodeComponent implements OnInit {
  userCode: IUserCode = {} as IUserCode; // creating an empty IUserCode, object of any type cast to IUserCode

  constructor(private submitCodeService: SubmitCodeService, private toastrService: ToastrService, private winnersListService: WinnersListService, private router: Router) { // userCode.code has to be initialized here as an empty object as well
    this.userCode.code = {} as ICode;
  }

  ngOnInit() {
  }

  submit() {
    this.submitCodeService.submitCode(this.userCode).subscribe(
      (result) => {
        this.toastrService.success(result.awardDescription);
        this.router.navigate(['winners']);
        // this.winnersListService.getAllWinners();
      },
      (error) => {
        this.toastrService.error(error.error.exceptionMessage, 'Error!');
      });
  }

}
