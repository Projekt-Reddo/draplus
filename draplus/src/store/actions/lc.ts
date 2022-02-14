import { INITLC } from ".";

export const initLC = (data: any) => {
    return { type: INITLC, payload: data };
};
