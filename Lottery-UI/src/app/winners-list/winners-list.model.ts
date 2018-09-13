export interface IUserCodeAward {
    userCode: IUserCode;
    award: IAward;
    wonAt: Date;
}

export interface IUserCode {
    firstName: string;
    lastName: string;
    eMail: string;
    code: ICode;
}

export interface IAward {
    awardName: string;
    awardDescription: string;
}

export interface ICode {
    codeValue: string;
}