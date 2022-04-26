import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Landmark } from 'src/app/models/Landmark';
import { AuthService } from 'src/app/services/auth.service';
import { LandmarkService } from 'src/app/services/landmark.service';

@Component({
  selector: 'app-list-landmarks',
  templateUrl: './list-landmarks.component.html',
  styleUrls: ['./list-landmarks.component.css']
})
export class ListLandmarksComponent implements OnInit {
  landmarks!: Array<Landmark>;
  isLogged: boolean = false;

  constructor(
    private landmarkService: LandmarkService,
    private router: Router,
    private authService: AuthService,
    private toastrService: ToastrService) { }

  ngOnInit(): void {
    this.fetchLandmarks();
    this.isLogged = this.authService.isAuthenticated();
  }

  fetchLandmarks() {
    this.landmarkService.getLandmarks().subscribe(l => {
      this.landmarks = l;
    });
  }

  deleteLandmark(id: number): void {
    this.landmarkService.deleteLandmark(id).subscribe(res => {
      this.toastrService.success("Successfully deleted landmark!");
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
