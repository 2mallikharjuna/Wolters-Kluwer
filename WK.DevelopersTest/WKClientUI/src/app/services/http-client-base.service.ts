import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root',
})

export class HttpClientBase {
    private headers: HttpHeaders;
    private baseUrl: string = environment.api.baseUrl;

    constructor(private httpClient: HttpClient) {
        this.headers = new HttpHeaders().set('Content-Type', 'application/json');
    }

    get<T>(url: string, httpParams?: HttpParams): Observable<T> {
        const requestUrl = `${this.baseUrl}/${url}`;
        return this.httpClient
            .get<T>(requestUrl, {
                params: httpParams ? httpParams : undefined,
                headers: this.headers ? this.headers : undefined,
            })
            .pipe(catchError(this.handleError));
    }

    post<T, K>(url: string, model: K | null, httpParams?: HttpParams): Observable<T> {
        const requestUrl = `${this.baseUrl}/${url}`;
        
        return this.httpClient
            .post<T>(requestUrl, model, {
                params: httpParams ? httpParams : undefined,
                headers: this.headers ? this.headers : undefined,
            })
            .pipe(catchError(this.handleError));
    }    

    put<T, K>(url: string, model: K | null, httpParams?: HttpParams): Observable<T> {
        const requestUrl = `${this.baseUrl}/${url}`;
        
        return this.httpClient
            .put<T>(requestUrl, model, {
                params: httpParams ? httpParams : undefined,
                headers: this.headers ? this.headers : undefined,
            })
            .pipe(catchError(this.handleError));
    }

    delete<T>(url: string, httpParams?: HttpParams): Observable<T> {
        const requestUrl = `${this.baseUrl}/${url}`;
        
        return this.httpClient
            .delete<T>(requestUrl, {
                params: httpParams ? httpParams : undefined,
                headers: this.headers ? this.headers : undefined,
            })
            .pipe(catchError(this.handleError));
    }

    private handleError(error: HttpErrorResponse) {
        return throwError(error);
    }
}