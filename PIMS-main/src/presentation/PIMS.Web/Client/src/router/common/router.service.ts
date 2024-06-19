export enum DefaultRoutePathes {
  NotSet = '',
  Search = '/',
  LoginForms = '/loginForms',
  Error = '/error',
  Register='/register',
}

export enum DefaultRouteNames {
  NotSet = '',
  Search = 'search',
  LoginForms = 'loginForms',
  Error = 'error',
  Register='registerForms'
}
export class DefaultRouteSettings {
  public static LoginForms: RouteSettings = {
    Path: DefaultRoutePathes.LoginForms,
    Name: DefaultRouteNames.LoginForms
  }
  public static RegisterForms: RouteSettings = {
    Path: DefaultRoutePathes.Register,
    Name: DefaultRouteNames.Register
  }
  public static Error: RouteSettings = {
    Path: DefaultRoutePathes.Error,
    Name: DefaultRouteNames.Error
  }
  public static Search: RouteSettings = {
    Path: DefaultRoutePathes.Search,
    Name: DefaultRouteNames.Search
  }
}
export class RouteSettings {
  Path: DefaultRoutePathes = DefaultRoutePathes.Error
  Name: DefaultRouteNames = DefaultRouteNames.NotSet
}
