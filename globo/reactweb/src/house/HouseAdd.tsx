import React from 'react';
import { useAddHouse } from '../hooks/HouseHooks';
import { House } from '../types/house';
import { HouseForm } from './HouseForm';
import { ValidationSummary } from '../ValidationSummary';

export const HouseAdd = () => {
  const addHouseMutation = useAddHouse();

  const house: House = {
    address: '',
    country: '',
    description: '',
    price: 0,
    id: 0,
    photo: '',
  };

  return (
    <>
      {addHouseMutation.isError && <ValidationSummary error={addHouseMutation.error} />}
      <HouseForm house={house} submitted={(h) => addHouseMutation.mutate(h)} />
    </>
  );
};
