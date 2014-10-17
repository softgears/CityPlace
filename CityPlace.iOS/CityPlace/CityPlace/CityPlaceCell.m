//
//  CityPlaceCell.m
//  CityPlace
//
//  Created by Yuri Korshev on 17.10.14.
//  Copyright (c) 2014 SoftGears. All rights reserved.
//

#import "CityPlaceCell.h"
#import <QuartzCore/QuartzCore.h>


@implementation CityPlaceCell

@synthesize imageView;
@synthesize label;
@synthesize imageUrl;

- (void)awakeFromNib {
    // Initialization code
    imageView.layer.backgroundColor=[[UIColor clearColor] CGColor];
    imageView.layer.cornerRadius=20.5f;
    imageView.layer.borderWidth=1.0;
    imageView.layer.masksToBounds = YES;
    imageView.layer.borderColor=[[UIColor whiteColor] CGColor];
}

- (void)setSelected:(BOOL)selected animated:(BOOL)animated {
    [super setSelected:selected animated:animated];

    // Configure the view for the selected state
}

@end
