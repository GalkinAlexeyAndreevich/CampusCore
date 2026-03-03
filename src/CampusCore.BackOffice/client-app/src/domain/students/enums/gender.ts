export const Gender = {
  Male: 1,
  Female: 2,
} as const;

export type Gender = (typeof Gender)[keyof typeof Gender];

export const GenderUtils = {
  getDisplayName(gender: Gender): string {
    switch (gender) {
      case Gender.Male:
        return "Мужской";
      case Gender.Female:
        return "Женский";
      default:
        return "";
    }
  },
} as const;
