import { Effect } from 'dva';
import { Reducer } from 'redux';
import { TableListData } from '@/data';

import { queryCurrent, query as queryUsers } from '@/services/user';

// export interface StateType {
//   data: TableListData;
//   currentUser: any;
// }

export interface CurrentUser {
  avatar?: string;
  name?: string;
  title?: string;
  group?: string;
  signature?: string;
  tags?: {
    key: string;
    label: string;
  }[];
  unreadCount?: number;
}

export interface UserModelState {
  data?: TableListData;
  currentUser?: CurrentUser;
}

export interface UserModelType {
  namespace: 'user';
  state: UserModelState;
  effects: {
    fetch: Effect;
    fetchCurrent: Effect;
    all: Effect;
  };
  reducers: {
    saveCurrentUser: Reducer<UserModelState>;
    changeNotifyCount: Reducer<UserModelState>;
    storeUsers: Reducer<UserModelState>;
  };
}

const UserModel: UserModelType = {
  namespace: 'user',

  state: {
    currentUser: {},
    data: {
      list: [],
      pagination: {},
    },
  },

  effects: {
    *fetch(_, { call, put }) {
      const response = yield call(queryUsers);
      yield put({
        type: 'save',
        payload: response,
      });
    },
    *fetchCurrent(_, { call, put }) {
      const response = yield call(queryCurrent);
      yield put({
        type: 'saveCurrentUser',
        payload: response,
      });
    },
    *all(_, { call, put }) {
      const response = yield call(queryUsers);
      yield put({
        type: 'storeUsers',
        payload: response,
      });
    },
  },

  reducers: {
    saveCurrentUser(state, action) {
      return {
        ...state,
        currentUser: action.payload || {},
      };
    },
    changeNotifyCount(
      state = {
        currentUser: {},
      },
      action,
    ) {
      return {
        ...state,
        currentUser: {
          ...state.currentUser,
          notifyCount: action.payload.totalCount,
          unreadCount: action.payload.unreadCount,
        },
      };
    },
    storeUsers(state, { payload: { data } }) {
      return {
        ...state,
        data: data,
      };
    },
  },
};

export default UserModel;
