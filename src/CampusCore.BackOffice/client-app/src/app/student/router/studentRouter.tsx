import React from 'react';
import { Route } from 'react-router-dom';
import { StudentPage } from '../studentPage';
import { StudentLink } from './studentLink';

export function StudentRouter() {
	return (
		<>
			<Route path={StudentLink.index} element={<StudentPage />} />
		</>
	);
}
