import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Visit } from 'src/app/models/Visit';
import { AuthService } from 'src/app/services/auth.service';
import { VisitService } from 'src/app/services/visit.service';

@Component({
  selector: 'app-visit',
  templateUrl: './visit.component.html',
  styleUrls: ['./visit.component.css']
})
export class VisitComponent implements OnInit {
  visits: Array<Visit> = [];
  constructor(
    private visitService: VisitService) {
      this.visitService.getAllVisitedLandmarksForUser().subscribe(res => {
        this.visits = this.transformDates(res);
      });
    }

  ngOnInit(): void {
  }

  transformDates(visitArr: Array<Visit>): Array<Visit> {
    const len = visitArr.length;
    for(let i = 0; i < len; i++) {
      visitArr[i].visitedOn = new Intl.DateTimeFormat("en-GB", { 
        dateStyle: "short", 
        timeStyle: "short",
        hour12: false
      }).format(new Date(visitArr[i].visitedOn));
    }
    return visitArr;
  }
}