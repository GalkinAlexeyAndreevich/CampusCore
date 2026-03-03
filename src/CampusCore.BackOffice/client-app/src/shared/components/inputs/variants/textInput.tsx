import { TextField } from '@mui/material';
import React, { ChangeEvent, HTMLInputTypeAttribute } from 'react';
import { InputProps as DefaultProps } from '../input';

export interface Props extends DefaultProps {
	placeholder?: string;

	value: string | null;
	onChange: (value: string | null) => void;

	required?: boolean;
	inputType?: HTMLInputTypeAttribute;
}

export function TextInput(props: Props) {
	function onChange(event: ChangeEvent<HTMLInputElement>) {
		let value: string | null = event.target.value;
		if (String.isNullOrWhitespace(value)) value = null;

		props.onChange(value);
	}

	return (
		<TextField
			type={props.inputType ?? 'text'}
			label={props.title}
			slotProps={props.inputType === 'date' ? { inputLabel: { shrink: true } } : undefined}
			autoComplete={props.inputType === 'password' ? 'new-password' : undefined}
			placeholder={props.placeholder}
			className={props.className}
			size={props.size}
			sx={props.sx}
			value={props.value ?? ''}
			onChange={onChange}
			required={props.required}
			disabled={props.disabled}
			fullWidth
		/>
	);
}
