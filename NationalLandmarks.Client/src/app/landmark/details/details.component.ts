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
  form: FormGroup;
  landmarkGrade: number = 0;
  landmarkTotalVisits: number = 0;

  constructor(
    private route: ActivatedRoute,
    private landmarkService: LandmarkService,
    private fb: FormBuilder,
    private visitService: VisitService,
    private toastrService: ToastrService,
    private router: Router,
    private authService: AuthService) {
      this.isLogged = this.authService.isAuthenticated();
    this.fetchData();
    this.form = this.fb.group({
      grade: [0]
    });
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
      this.landmarkGrade = Math.round((res.grades.reduce((sum, current) => sum + current, 0) / res.totalVisits) * 100) / 100;
      this.landmarkTotalVisits = res.totalVisits;
    });
  }

  ngOnInit(): void {
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
    var visit: Visit = {
      landmarkId: this.landmark.id,
      grade: this.form.controls['grade'].value,
      landmark: {
        name: '',
        isNationalLandmark: false,
        townName: '',
        imageUrl: ''
      },
      visitedOn: new Date()
    }

    this.visitService.visitLandmark(visit).subscribe(res => {
      this.toastrService.success("Successfully visited!");
      this.router.navigate(['/landmarks']);
    });
  }

}
