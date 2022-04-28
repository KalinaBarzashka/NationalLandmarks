import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { map, mergeMap } from 'rxjs';
import { Landmark } from 'src/app/models/Landmark';
import { AuthService } from 'src/app/services/auth.service';
import { LandmarkService } from 'src/app/services/landmark.service';
import { VisitService } from 'src/app/services/visit.service';
import { Visit } from '../../models/Visit';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit {
  isLogged: boolean = false;
  id!: number;
  landmark!: Landmark;
  stars: Array<any> = [
    {
      value: 1,
      checked: false,
      id: "first-star"
    },
    {
      value: 2,
      checked: false,
      id: "second-star"
    },
    {
      value: 3,
      checked: false,
      id: "third-star"
    },
    {
      value: 4,
      checked: false,
      id: "fourth-star"
    },
    {
      value: 5,
      checked: false,
      id: "fifth-star"
    }
  ]
  form!: FormGroup;
  landmarkGrade: number = 0;
  landmarkTotalVisits: number = 0;
  status: string = 'INIT';
  currentLatitude: number = 0;
  currentLongitude: number = 0;
  currentAccuracy: number = 0;
  canVisit: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private landmarkService: LandmarkService,
    private fb: FormBuilder,
    private visitService: VisitService,
    private toastrService: ToastrService,
    private router: Router,
    private authService: AuthService) {
  }

  ngOnInit(): void {
    this.status = 'LOADING';
    this.isLogged = this.authService.isAuthenticated();
    this.fetchData();
    this.form = this.fb.group({
      grade: [0]
    });

    navigator.geolocation.getCurrentPosition((pos: GeolocationPosition) => {
      var crd = pos.coords;
      this.currentLatitude = crd.latitude;
      this.currentLongitude = crd.longitude;
      this.currentAccuracy = crd.accuracy;
      this.canVisit = true;
    }, (err: GeolocationPositionError) => {
      this.toastrService.error(`Couldn't get location! ${err.message}`);
      //visable visit btn
      this.canVisit = false;
    }, this.options);
  }

  fetchData(): void {
    this.route.params.pipe(
      map(params => {
        const id = params['id'];
        return id;
      }),
      mergeMap(id => this.landmarkService.getLandmark(id))//switchMap()
    ).subscribe(res => {
      this.landmark = res;
      if(res.grades.length > 0) {
        this.landmarkGrade = Math.round((res.grades.reduce((sum, current) => sum + current, 0) / res.totalVisits) * 100) / 100;
      }
      
      this.landmarkTotalVisits = res.totalVisits;
      this.status = 'SUCCESS';
    });
  }

  handleStarClick(value: number): void {
    for (let i = 0; i < this.stars.length; i++) {
      let currStar = this.stars[i];
      let currValue = currStar.value;
      if (currValue <= value) {
        currStar.checked = true;
      } else {
        currStar.checked = false;
      }
    };
  }

  handleSubmit() {
    var distant = Math.round(this.getDistanceFromLatLonInKm(this.currentLatitude, this.currentLongitude, this.landmark.latitude, this.landmark.longitude) * 100) / 100;

    //console.log(distant);
    //console.log(this.currentLatitude);
    //console.log(this.currentLongitude);
    //console.log(this.landmark.latitude);
    //console.log(this.landmark.longitude);
    //console.log(this.currentAccuracy / 1000.0);

    if(distant > 20){
      this.toastrService.warning("Cannot visit landmark, please get closer!");
      return;
    }

    var visit: Visit = {
      landmarkId: this.landmark.id,
      grade: this.form.controls['grade'].value,
      landmark: {
        name: '',
        isNationalLandmark: false,
        townName: '',
        imageUrl: ''
      },
      visitedOn: ''
    }

    this.visitService.visitLandmark(visit).subscribe(res => {
      this.toastrService.success("Successfully visited!");
      this.router.navigate(['/landmarks/1']);
    });
  }

  options = {
    enableHighAccuracy: true,
    timeout: 5000,
    maximumAge: 0 //default value
  };

  /* 
Title: Get Distance from two Latitude / Longitude in Kilometers.

Description: This Javascript snippet calculates great-circle distances between the two points 
—— that is, the shortest distance over the earth’s surface; using the ‘Haversine’ formula.
*/

  private getDistanceFromLatLonInKm(lat1: number, lon1: number, lat2: number, lon2: number) {
    var R = 6371; // Radius of the earth in kilometers
    var dLat = this.deg2rad(lat2 - lat1); // deg2rad below
    var dLon = this.deg2rad(lon2 - lon1);
    var a =
      Math.sin(dLat / 2) * Math.sin(dLat / 2) +
      Math.cos(this.deg2rad(lat1)) * Math.cos(this.deg2rad(lat2)) *
      Math.sin(dLon / 2) * Math.sin(dLon / 2);
    var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
    var d = R * c; // Distance in KM
    return d;
  }

  private deg2rad(deg: number): number {
    return deg * (Math.PI / 180)
  }
}
