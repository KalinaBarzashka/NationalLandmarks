import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Landmark } from 'src/app/models/Landmark';
import { LandmarkService } from 'src/app/services/landmark.service';

@Component({
  selector: 'app-list-landmarks',
  templateUrl: './list-landmarks.component.html',
  styleUrls: ['./list-landmarks.component.css']
})
export class ListLandmarksComponent implements OnInit {
  landmarks!: Array<Landmark>;
  constructor(private landmarkService: LandmarkService, private router: Router) { }

  ngOnInit(): void {
    this.fetchLandmarks();
  }

  fetchLandmarks() {
    this.landmarkService.getLandmarks().subscribe(l => {
      this.landmarks = l;
    });
  }

  deleteLandmark(id: number): void {
    this.landmarkService.deleteLandmark(id).subscribe(res => {
      this.fetchLandmarks();
    });
  }

  editLandmark(id: number) {
    this.router.navigate(['landmarks', id, 'edit']);
  }

  //routeToLandmark(id: number) {
  //  this.router.navigate(['landmarks', id]);
  //}

}
