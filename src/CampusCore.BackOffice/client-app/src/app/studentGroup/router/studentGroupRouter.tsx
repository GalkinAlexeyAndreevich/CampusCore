import React from 'react';
import { Route } from 'react-router-dom';
import { StudentGroupPage } from '../studentGroupPage';
import { StudentGroupLink } from './studentGroupLink';

export function StudentGroupRouter() {
	return (
		<>
			<Route path={StudentGroupLink.index} element={<StudentGroupPage />} />
		</>
	);
}
