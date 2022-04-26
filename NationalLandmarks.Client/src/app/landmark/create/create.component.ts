import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LandmarkService } from 'src/app/services/landmark.service';
import { ToastrService } from 'ngx-toastr';
import { TownService } from 'src/app/services/town.service';
import { Town } from 'src/app/models/Town';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent {
  landmarkForm!: FormGroup;
  towns: Array<Town> = [];

  get name() { return this.landmarkForm.get('name'); }
  get isNationalLandmark() { return this.landmarkForm.get('isNationalLandmark'); }
  get description() { return this.landmarkForm.get('description'); }
  get town() { return this.landmarkForm.get('town'); }
  get address() { return this.landmarkForm.get('address'); }
  get latitude() { return this.landmarkForm.get('latitude'); }
  get longitude() { return this.landmarkForm.get('longitude'); }
  get imageUrl() { return this.landmarkForm.get('imageUrl'); }
  get opens() { return this.landmarkForm.get('opens'); }
  get closes() { return this.landmarkForm.get('closes'); }
  get worksOnWeekends() { return this.landmarkForm.get('worksOnWeekends'); }
  get email() { return this.landmarkForm.get('email'); }
  get phone() { return this.landmarkForm.get('phone'); }
  get website() { return this.landmarkForm.get('website'); }

  constructor(
    private fb: FormBuilder, 
    private landmarkService: LandmarkService,
    private townService: TownService,
    private toastrService: ToastrService,
    private router: Router) {
    this.landmarkForm = this.fb.group({
      'name': ['', [Validators.required]],
      'isNationalLandmark': [true, [Validators.required]],
      'description': ['', [Validators.required]],
      'townId': ['', [Validators.required]],
      'address': ['', [Validators.required]],
      'latitude': ['', [Validators.required]],
      'longitude': ['', [Validators.required]],
      'imageUrl': ['', [Validators.required]],
      'opens': [''],
      'closes': [''],
      'worksOnWeekends': [false],
      'email': [''],
      'phone': [''],
      'website': [''],
    });

    this.townService.getCities().subscribe(res => {
      this.towns = res;
    });
  }

  changeCity(e: any) {
    this.town?.setValue(e.target.value, {
      onlySelf: true
    })
  }

  createHandler(): void {
    console.log(this.landmarkForm.value);
    this.landmarkService.create(this.landmarkForm.value).subscribe(res => {
      this.toastrService.success("Successfully created landmark!");
      this.router.navigate(['/landmarks']);
    });
  }

}
