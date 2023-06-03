import { House } from '../types/house';
import config from '../config';
import { useQuery } from 'react-query';
import { AxiosError } from 'axios';
import axios from 'axios';

const useFetchHouses = () => {
  return useQuery<House[], AxiosError>('houses', () =>
    axios.get(`${config.baseApiUrl}/houses`).then((res) => res.data)
  );
};

export default useFetchHouses;
