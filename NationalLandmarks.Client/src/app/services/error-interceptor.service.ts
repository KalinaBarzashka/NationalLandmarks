import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, retry, throwError } from 'rxjs';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class ErrorInterceptorService implements HttpInterceptor {

  constructor(private toastrService: ToastrService) { }
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    
    return next.handle(req).pipe(
      retry(1),
      catchError(err => {
        let message = '';
        if(err.status === 401) {
          //refresh token or navigate to login
          message = 'Token has expired or you are not authorized for this functionality!';
        }
        else if(err.status === 404){
          //some custom message
          message = '404!';
        }
        else if(err.status === 400){
          //some message
          message = '400!';
        }
        else{
          //some global message
          message = 'Unexpected error!';
        }
        this.toastrService.error(message, "Error");

        return throwError(err);
      })
    );
  }
}
