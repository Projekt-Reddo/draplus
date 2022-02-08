import { createStore, Store } from "redux";
import rootReducer from "./reducers";

type RootState = ReturnType<typeof rootReducer>;

const store: Store<RootState, ActionType> & {
    dispatch: DispatchType;
} = createStore(rootReducer);

export default store;
