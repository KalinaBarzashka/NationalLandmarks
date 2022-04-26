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
        this.visits = res;
      });
    }

  ngOnInit(): void {
  }

}
