import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class BusyService {
  busyRequestCount = 0;

  constructor(private spinnerServices: NgxSpinnerService) { }

  busy(){
    this.busyRequestCount++;
    this.spinnerServices.show(undefined, {
      type: 'square-jelly-box',
      bdColor: 'rgba(255,255,255,0)',
      color: '#32fbe2'
    });
  }

  idle(){
    this.busyRequestCount--;
    if(this.busyRequestCount <= 0) {
      this.busyRequestCount = 0;
      this.spinnerServices.hide();
    }
  }
}
