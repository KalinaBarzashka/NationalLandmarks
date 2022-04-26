import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { map, mergeMap } from 'rxjs';
import { Landmark } from 'src/app/models/Landmark';
import { LandmarkService } from 'src/app/services/landmark.service';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {
  landmarkForm!: FormGroup;
  landmarkId!: number;
  landmark!: Landmark;
  landmarkName: string = '';

  constructor(
    private fb: FormBuilder, 
    private route: ActivatedRoute, 
    private landmarkService: LandmarkService,
    private router: Router,
    private toastrService: ToastrService) 
  {
    this.landmarkForm = this.fb.group({
      'id': [''],
      'isNationalLandmark': [''],
      'description': ['', [Validators.required]],
      'opens': [''],
      'closes': [''],
      'worksOnWeekends': [''],
      'email': [''],
      'phone': [''],
      'website': [''],      
      'imageUrl': [, [Validators.required]]
    });
  }

  ngOnInit(): void {
    this.fetchData();
  }

  editHandler(): void {
    this.landmarkService.editLandmark(this.landmarkForm.value).subscribe(res => {
      this.toastrService.success("Successfully edited landmark!");
      this.router.navigate(['/landmarks', this.landmarkForm.controls['id'].value]);
    });
  }

  fetchData() {
    this.route.params
      .pipe(
        map(params => {
          const id: number = params['id'];
          return id;
        }),
        mergeMap(
          id => this.landmarkService.getLandmark(id)
        ))
      .subscribe(res => {
        this.landmark = res;
        this.landmarkName = res.name;
        this.landmarkForm = this.fb.group({
          'id': [this.landmark.id],
          'isNationalLandmark': [this.landmark.isNationalLandmark],
          'description': [this.landmark.description, [Validators.required]],
          'opens': [this.landmark.opens],
          'closes': [this.landmark.closes],
          'worksOnWeekends': [this.landmark.worksOnWeekends],
          'email': [this.landmark.email],
          'phone': [this.landmark.phone],
          'website': [this.landmark.website],      
          'imageUrl': [this.landmark.imageUrl, [Validators.required]]
        });
      });
  }
}
