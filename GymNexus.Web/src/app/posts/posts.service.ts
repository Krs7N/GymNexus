import { Injectable, Injector } from '@angular/core';
import { CrudService } from '../core/services/crud.service';
import { PostModel } from './post-model';
import { Observable } from 'rxjs';
import { PostViewModel } from './post-view-model';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class PostsService extends CrudService<PostModel> {

  constructor(injector: Injector) { 
    super(injector);
  }

  getAllPosts(): Observable<PostViewModel[]> {
    return this.httpClient.get<PostViewModel[]>(`${environment.apiBaseUrl}/${this.APIUrl}`);
  }

  override getResourceUrl(): string {
    return 'posts';
  }
}
