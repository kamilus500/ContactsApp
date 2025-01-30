import { Injectable } from '@angular/core';
import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
  HttpErrorResponse,
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { MessageService } from 'primeng/api';

@Injectable()
export class ApiInterceptor implements HttpInterceptor {
  constructor(private messageService: MessageService) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError((error: HttpErrorResponse) => {
        if (!navigator.onLine) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'No internet connection' });
        } else if (error.status === 0) {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'No connection to the API server' });
        } else {
            this.messageService.add({ severity: 'error', summary: 'error', detail: `${error.status} - ${error.message}`})
        }
        return throwError(() => error);
      })
    );
  }
}