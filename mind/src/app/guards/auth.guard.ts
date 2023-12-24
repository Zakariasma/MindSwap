import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { CookieService } from 'ngx-cookie-service';

export function authGuard(route: ActivatedRouteSnapshot, state: RouterStateSnapshot, cookieService: CookieService, router: Router): boolean {
  const token = cookieService.get('token');
  if (token && token !== '') {
    return true;
  } else {
    router.navigate(['/login']);
    return false;
  }
}
