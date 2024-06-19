export class ValidatorDescription {
  Rules: ((v: any) => any)[] = []
  Length: number = 0
}
export class CommonValidators {
  public UserName: ValidatorDescription = {    
    Rules: [
      (v: any) => !!v || 'Логин пользователя обязательно должен быть указан',
      (v: any) => (v && v.length <= 255) || 'Логин пользователя должен быть не более 255 символов'
    ],
    Length: 255
  }
  public FirstName: ValidatorDescription = {    
    Rules: [
      (v: any) => !!v || 'Имя обязательно должна быть указана',
      (v: any) => (v && v.length <= 255) || 'Имя пользователя должно быть не более 255 символов'
    ],
    Length: 255
  }
  public MiddleName: ValidatorDescription = {
    Rules: [    
      (v: any) => (v && v.length <= 255) || 'Длина Отчества пользователя должна быть не более 255 символов'
    ],
    Length: 255
  }
  public LastName: ValidatorDescription = {
    Rules: [
      (v: any) => !!v || 'Фамилия обязательно должна быть указана',
      (v: any) => (v && v.length <= 255) || 'Длина Фамилии пользователя должна быть не более 255 символов'
    ],
    Length: 255
  }
  public Email: ValidatorDescription = {
    Rules: [      
      (v: any) => (v && v.length <= 255) || 'Длина E-mail пользователя должна быть не более 255 символов'
    ],
    Length: 255
  }
  public Required: ValidatorDescription = {
    Rules: [(v: any) => !!v || 'Обязательное поле'],
    Length: 0
  }
}
export const commonValidatorsCore: CommonValidators = new CommonValidators()
