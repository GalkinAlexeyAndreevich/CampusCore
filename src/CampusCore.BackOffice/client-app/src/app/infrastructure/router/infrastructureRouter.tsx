import React from 'react';
import { Route } from 'react-router-dom';
import { InfrastructureLink } from './infrastructureLink';
import { StudentPage } from '../../student/studentPage';

export function InfrastructureRouter() {
	return (
		<>
			<Route path={InfrastructureLink.index} element={<StudentPage />} />
		</>
	);
}
