import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Persona } from '../interfaces/persona';

@Injectable({
  providedIn: 'root'
})
export class PersonasService {
  urlWebAPI = 'https://ui20251201142043-dnhvdfbxdbh9bnbt.spaincentral-01.azurewebsites.net/api/personas';
  
  http = inject(HttpClient);

  getPersonas(): Observable<Persona[]> {
    return this.http.get<Persona[]>(this.urlWebAPI);
  }
}